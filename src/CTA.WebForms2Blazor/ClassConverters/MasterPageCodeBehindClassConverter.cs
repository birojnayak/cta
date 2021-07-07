﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CTA.WebForms2Blazor.FileInformationModel;
using CTA.WebForms2Blazor.Helpers;
using CTA.WebForms2Blazor.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CTA.WebForms2Blazor.ClassConverters
{
    public class MasterPageCodeBehindClassConverter : ClassConverter
    {
        public MasterPageCodeBehindClassConverter(
            string relativePath,
            string sourceProjectPath,
            SemanticModel sourceFileSemanticModel,
            TypeDeclarationSyntax originalDeclarationSyntax,
            INamedTypeSymbol originalClassSymbol,
            TaskManagerService taskManager)
            : base(relativePath, sourceProjectPath, sourceFileSemanticModel, originalDeclarationSyntax, originalClassSymbol, taskManager)
        {
            // TODO: Register with the necessary services
        }

        public override async Task<IEnumerable<FileInformation>> MigrateClassAsync()
        {
            LogStart();

            // NOTE: For now we make no code modifications, just to be
            // ready for the demo and produces files
            var sourceClassComponents = GetSourceClassComponents();

            DoCleanUp();
            LogEnd();

            return new[] { new FileInformation(GetNewRelativePath(), Encoding.UTF8.GetBytes(sourceClassComponents.FileText)) };
        }

        private string GetNewRelativePath()
        {
            // TODO: Potentially remove certain folders from beginning of relative path
            var newRelativePath = FilePathHelper.AlterFileName(_relativePath,
                oldExtension: Constants.MasterPageCodeBehindExtension,
                newExtension: Constants.RazorCodeBehindFileExtension);

            return Path.Combine(Constants.RazorLayoutDirectoryName, newRelativePath);
        }
    }
}
