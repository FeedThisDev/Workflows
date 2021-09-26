using System;
using System.Collections.Generic;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;

namespace Workflows.SamplePluginLib.Inputs
{
    public class DetectableLib : IPluginLibrary
    {
        public string LibraryName => "SampleOutputs";

        public PluginCategoriesType Category => PluginCategoriesType.Output;

        public List<PluginModule> PluginModules => _pluginModules;

        List<PluginModule> _pluginModules = new List<PluginModule>();

        public DetectableLib()
        {
            var docConfigVals = new List<BaseInputComponent>();
            BaseInputComponent docFilePath = new BaseInputComponent()
            {
                Label = " Word Document",
                DefaultValue = "C:\\sample.docx",
                InputType = InputTypes.FilePicker,
                Value = "C:\\sample.docx",
                Filter = "*.docx"
            };
            docConfigVals.Add(docFilePath);

            PluginModule docSelector = new PluginModule("Word Document", "/Assets/DocFile.png", docConfigVals);

            _pluginModules.Add(docSelector);
        }
    }
}
