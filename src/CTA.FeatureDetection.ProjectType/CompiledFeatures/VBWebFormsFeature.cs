﻿using Codelyzer.Analysis;
using CTA.FeatureDetection.Common.Extensions;
using CTA.FeatureDetection.Common.Models.Features.Base;

namespace CTA.FeatureDetection.ProjectType.CompiledFeatures
{
    public class VBWebFormsFeature : CompiledFeature
    {
        /// <summary>
        /// Determines if a project is an VB web forms project by looking at file extensions and nuget references
        /// </summary>
        /// <param name="analyzerResult"></param>
        /// <returns>Whether a project is an VB web forms project or not</returns>
        public override bool IsPresent(AnalyzerResult analyzerResult)
        {
            var project = analyzerResult.ProjectResult;
            var isPresent = (project.ContainsFileWithExtension(Constants.VbClassExtension, true) && project.ProjectFilePath.EndsWith(Constants.VbProjExtension)) && (project.ContainsFileWithExtension(Constants.AspxExtension, true)
                || project.ContainsDependency(Constants.WebFormsScriptManagerIdentifier) || project.ContainsDependency(Constants.WebFormsWebOptimizationIdentifier));

            return isPresent;
        }

    }
}
