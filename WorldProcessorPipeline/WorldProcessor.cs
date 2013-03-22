using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = System.String;

namespace WorldProcessorPipeline
{
    //[ContentProcessor(DisplayName = "WorldProcessorPipeline.ContentProcessor1")]
    [ContentProcessor]
    public class WorldDescriptionProcessor : ContentProcessor<WorldContainer, WorldData>
    {
        #region Overrides

        public override WorldDescriptorContent Process(WorldDescriptorContainer container, ContentProcessorContext context)
        {
            //System.Diagnostics.Debugger.Launch();

            if (string.IsNullOrEmpty(container.Map))
            {
                const string EX_MESSAGE = "Map data can not be null";
                throw new InvalidOperationException(EX_MESSAGE);
            }

            if (!string.IsNullOrEmpty(container.ItemsDefinitionFile))
            {
                OverridePropertiesFromExternalSource(container, context);
            }
            var descriptor = new WorldDescriptorContent(container);
            descriptor.Translate();

            return descriptor;
        }

        #endregion
        #region Private methods

        private void OverridePropertiesFromExternalSource(WorldDescriptorContainer container, ContentProcessorContext context)
        {
            var itemsDefinitionReference = new ExternalReference<ContentItem>(container.ItemsDefinitionFile);
            var itemsDefinition = context.BuildAndLoadAsset<ContentItem, MapItemsDefinition>(itemsDefinitionReference, null);

            container.BonusProperties = itemsDefinition.BonusProperties;
            container.DecorationProperties = itemsDefinition.DecorationProperties;
            container.EnemyProperties = itemsDefinition.EnemyProperties;
            container.GroundProperties = itemsDefinition.GroundProperties;
            container.HeroProperties = itemsDefinition.HeroProperties;
            container.ObstacleProperties = itemsDefinition.ObstacleProperties;
            container.SpecialMarkProperties = itemsDefinition.SpecialMarkProperties;
        }

        #endregion
    }
}