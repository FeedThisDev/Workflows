using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
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

        public List<Shared.Models.PluginCategory> Categories { get; private set; }

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
            Categories = new List<Shared.Models.PluginCategory>();

            var inputs = new PluginCategory()
            {
                Modules = new List<PluginModule>(),
                ImagePath = "Assets\\input.png",
                Name = "Inputs",
                CategoriesType = PluginCategoriesType.Input

            };
            Categories.Add(inputs);


            var logic = new PluginCategory()
            {
                Modules = new List<PluginModule>(),
                ImagePath = "Assets\\logic.png",
                Name = "Logic",
                CategoriesType = PluginCategoriesType.Logic

            };
            Categories.Add(logic);


            var actions = new PluginCategory()
            {
                Modules = new List<PluginModule>(),
                ImagePath = "Assets\\action.png",
                Name = "Actions",
                CategoriesType = PluginCategoriesType.Action

            };
            Categories.Add(actions);


            var outputs = new PluginCategory()
            {
                Modules = new List<PluginModule>(),
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
            var _libWatcher = new FileSystemWatcher("plugins\\", "*.dll");
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
                        foreach (var module in pluginLib.PluginModules)
                        {
                            var category = Categories.SingleOrDefault(x => x.CategoriesType == pluginLib.Category);
                            if(category == null)
                            {
                                _logger.Log("Couldn't find a category of type {0}", pluginLib.Category.ToString());
                                continue;
                            }

                            category.Modules.Add(module);
                        }
                    }
                }
            }
        }
    }
}
