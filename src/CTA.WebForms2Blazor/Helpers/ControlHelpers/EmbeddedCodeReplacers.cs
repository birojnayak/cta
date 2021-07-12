﻿using System;
using System.Text.RegularExpressions;

namespace CTA.WebForms2Blazor.Helpers.ControlHelpers
{
    public static class EmbeddedCodeReplacers
    {
        public const string EmbeddedExpressionRegexGroupName = "expr";
        public const string DirectiveNameRegexGroupName = "DirectiveName";

        //DataBind regex might have issue of taking front parentheses
        public static Regex DataBindRegex = new Regex("<%#\\W*(?<expr>[^%>]*)\\s*%>");
        public static Regex SingleExpRegex = new Regex("<%:\\s*(?<expr>[^%>]*)\\s*%>");
        public static Regex DirectiveRegex = new Regex("<%@\\s*(?<expr>[^%>]*)\\s*%>");
        public static Regex AspExpRegex = new Regex("<%\\$\\s*(?<expr>[^%>]*)\\s*%>");

        public static Regex DirectiveNameRegex = new Regex(@"(?<DirectiveName>[\S]+)");

        public static string ReplaceDataBind(Match m)
        {
            var expr = m.Groups[EmbeddedExpressionRegexGroupName].Value;
            var newValue = "@(" + expr + ")";
            return newValue;
        }
        
        public static string ReplaceSingleExpr(Match m)
        {
            //Same as Databind for now but can add features later
            var expr = m.Groups[EmbeddedExpressionRegexGroupName].Value;
            var newValue = "@(" + expr + ")";
            return newValue;
        }
        
        public static string ReplaceDirective(Match m)
        {
            var expr = m.Groups[EmbeddedExpressionRegexGroupName].Value;
            var directiveName = DirectiveNameRegex.Match(expr).Groups[DirectiveNameRegexGroupName].Value;
            var directiveConverter = SupportedControls.DirectiveRulesMap.ContainsKey(directiveName) ?
                SupportedControls.DirectiveRulesMap[directiveName] : SupportedControls.DefaultDirectiveConverter;

            return directiveConverter.ConvertDirective(directiveName, expr);
        }
        
        public static string ReplaceAspExpr(Match m)
        {
            throw new NotImplementedException();
        }
    }
}
