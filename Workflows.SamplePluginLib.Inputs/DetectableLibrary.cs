using System;
using System.Collections.Generic;
using Workflows.Shared.Contracts;
using Workflows.Shared.Models;

namespace Workflows.SamplePluginLib.Inputs
{
    public class DetectableLibrary : IPluginLibrary
    {
        public string LibraryName =>  "SampleInputs";

        public PluginCategoriesType Category => PluginCategoriesType.Input;

        public List<PluginModule> PluginModules => _pluginModules;

        List<PluginModule> _pluginModules = new List<PluginModule>();

        public DetectableLibrary()
        {
            var csvConfigVals = new List<BaseInputComponent>();
            BaseInputComponent csvFilePath = new BaseInputComponent()
            {
                Label = "CSV filepath: ",
                DefaultValue = "C:\\sample.csv",
                InputType = InputTypes.FilePicker,
                Value = "C:\\sample.csv",
                Filter = "*.csv"
            };
            csvConfigVals.Add(csvFilePath);

            PluginModule csvSelector = new PluginModule("CSV Input", "/Assets/CSVFile.png", csvConfigVals);

            var emailConfigVals = new List<BaseInputComponent>();
            BaseInputComponent emailSender = new BaseInputComponent()
            {
                Label = "Sender address: ",
                DefaultValue = "till@feedthis.dev",
                InputType = InputTypes.EMailAddress,
                Value = "till@feedthis.dev"
            };

            BaseInputComponent sendDate = new BaseInputComponent()
            {
                Label = "Received on Date: ",
                DefaultValue = DateTime.Now.Date,
                InputType = InputTypes.DatePicker,
                Value = DateTime.Now.Date
            };

            emailConfigVals.Add(emailSender);
            emailConfigVals.Add(sendDate);  

            PluginModule eMailAttachmentSelector = new PluginModule("E-Mail Input", "/Assets/email.png", emailConfigVals);

            _pluginModules.Add(eMailAttachmentSelector);
            _pluginModules.Add(csvSelector);
        }
    }
}
