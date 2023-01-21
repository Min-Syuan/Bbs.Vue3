﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    internal class ConstTemplateProvider : ProgramTemplateProvider
    {
        public ConstTemplateProvider(string modelName, string entityName) : base(modelName, entityName)
        {
            BuildPath = $@"{TemplateConst.BuildRootPath}\Yi.Framework.Domain.Shared\{TemplateConst.ModelName}\ConstClasses\{TemplateConst.EntityName}Const.cs";
            TemplatePath = $@"..\..\..\Template\Server\ConstTemplate.txt";
        }
    }
}
