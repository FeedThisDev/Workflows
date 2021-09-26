using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;
using Workflows.Shared.Services.Contracts;

namespace Workflows.UI.Services
{
    public class PluginService : ObservableObject, IPluginService
    {
        private const string _pluginDir = ".\\";

        public List<IPluginLibrary> PluginLibraries { get; private set; }

        public List<PluginCategory> Categories { get; private set; }

        private readonly ILoggingService _logger = Ioc.Default.GetRequiredService<ILoggingService>();


        public PluginService()
        {
            Init();
        }

        private void Init()
        {
            PluginLibraries = new List<IPluginLibrary>();
            CreateCategories();

            CreateFileWatcher();

        }

        private void CreateCategories()
        {
            Categories = new List<PluginCategory>();

            var inputs = new PluginCategory()
            {
                ImagePath = "/Assets/input.png",
                Name = "Inputs",
                CategoriesType = PluginCategoriesType.Input

            };
            Categories.Add(inputs);


            var logic = new PluginCategory()
            {
                ImagePath = "Assets\\logic.png",
                Name = "Logic",
                CategoriesType = PluginCategoriesType.Logic

            };
            Categories.Add(logic);


            var actions = new PluginCategory()
            {
                ImagePath = "Assets\\action.png",
                Name = "Actions",
                CategoriesType = PluginCategoriesType.Action

            };
            Categories.Add(actions);


            var outputs = new PluginCategory()
            {
                ImagePath = "Assets\\output.png",
                Name = "Outputs",
                CategoriesType = PluginCategoriesType.Output

            };
            Categories.Add(outputs);
        }

        private void CreateFileWatcher()
        {
            if (!Directory.Exists(_pluginDir))
                Directory.CreateDirectory(_pluginDir);

            RefreshPluginFolder();
            var _libWatcher = new FileSystemWatcher(".\\", "*.dll");
            _libWatcher.Created += OnCreated;
            _libWatcher.Deleted += OnDeleted;
            _libWatcher.IncludeSubdirectories = true;

            _libWatcher.EnableRaisingEvents = true;
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            RefreshPluginFolder();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            RefreshPluginFolder();
        }

        private void RefreshPluginFolder()
        {
            foreach (string assemblyPath in Directory.GetFiles(_pluginDir, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(assemblyPath);

                foreach (var typeInfo in assembly.DefinedTypes)
                {
                    if (typeInfo.ImplementedInterfaces.Contains(typeof(IPluginLibrary)))
                    {
                        var pluginLib = assembly.CreateInstance(typeInfo.FullName) as IPluginLibrary;

                        var category = Categories.SingleOrDefault(x => x.CategoriesType == pluginLib.Category);
                        if (category == null)
                        {
                            _logger.Log("Couldn't find a category of type {0}", pluginLib.Category.ToString());
                            continue;
                        }

                     
                        foreach (var module in pluginLib.PluginModules)
                        {
                            if (category.Modules.Any(x => x.Name == module.Name))
                                continue;

                            category.Modules.Add(module);
                            _logger.Log("Added module {0} from {1} to category {2}"
                          , module.Name
                          , pluginLib.LibraryName
                          , pluginLib.Category);
                        }

                        //bit ugly WPF hack to trigger DataTrigger Styles *sorry*
                        Categories = Categories.ToList();
                        WeakReferenceMessenger.Default.Send(new CategoriesChangedMessage(pluginLib.Category));
                    }
                }
            }

        }
    }
}
