﻿using HtmlAgilityPack;

namespace CTA.WebForms.TagConverters.TagTemplateConditions
{
    /// <summary>
    /// Represents a condition that is met if the <see cref="HtmlNode"/> to be
    /// converted has a specific attribute set to a specific value.
    /// </summary>
    public class HasAttributeWithValueTemplateCondition : TemplateCondition
    {
        /// <summary>
        /// The attribute name to check for in the the given <see cref="HtmlNode"/>'s
        /// attribute set.
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// The attribute value to compare against the <see cref="HtmlNode"/>'s
        /// specified attribute, if it is found.
        /// </summary>
        public string AttributeValue { get; set; }

        /// <inheritdoc/>
        public override bool Validate(bool isBaseCondition)
        {
            return base.Validate(isBaseCondition) && !string.IsNullOrEmpty(AttributeName) && !string.IsNullOrEmpty(AttributeValue);
        }

        /// <inheritdoc/>
        public override bool ConditionIsMet(HtmlNode node)
        {
            return AttributeName switch
            {
                "InnerHtml" => node.InnerHtml != null && node.InnerHtml.Equals(AttributeValue),
                _ => node.GetAttributeValue(AttributeName, null)?.Equals(AttributeValue) ?? false
            };
        }
    }
}
