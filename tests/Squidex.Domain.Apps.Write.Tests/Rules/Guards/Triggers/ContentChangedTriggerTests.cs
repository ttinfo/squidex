﻿// ==========================================================================
//  ContentChangedTriggerTests.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using FakeItEasy;
using Squidex.Domain.Apps.Core.Rules.Triggers;
using Squidex.Domain.Apps.Read;
using Squidex.Domain.Apps.Read.Schemas;
using Xunit;

namespace Squidex.Domain.Apps.Write.Rules.Guards.Triggers
{
    public class ContentChangedTriggerTests
    {
        private readonly IAppProvider appProvider = A.Fake<IAppProvider>();
        private readonly string appName = "my-app";

        [Fact]
        public async Task Should_add_error_if_schemas_ids_are_not_valid()
        {
            A.CallTo(() => appProvider.GetSchemaAsync(appName, A<Guid>.Ignored, false))
                .Returns(Task.FromResult<ISchemaEntity>(null));

            var trigger = new ContentChangedTrigger
            {
                Schemas = ImmutableList.Create(
                    new ContentChangedTriggerSchema()
                )
            };

            var errors = await RuleTriggerValidator.ValidateAsync(appName, trigger, appProvider);

            Assert.NotEmpty(errors);
        }

        [Fact]
        public async Task Should_not_add_error_if_schemas_is_null()
        {
            var trigger = new ContentChangedTrigger();

            var errors = await RuleTriggerValidator.ValidateAsync(appName, trigger, appProvider);

            Assert.Empty(errors);
        }

        [Fact]
        public async Task Should_not_add_error_if_schemas_is_empty()
        {
            var trigger = new ContentChangedTrigger
            {
                Schemas = ImmutableList<ContentChangedTriggerSchema>.Empty
            };

            var errors = await RuleTriggerValidator.ValidateAsync(appName, trigger, appProvider);

            Assert.Empty(errors);
        }

        [Fact]
        public async Task Should_not_add_error_if_schemas_ids_are_valid()
        {
            A.CallTo(() => appProvider.GetSchemaAsync(appName, A<Guid>.Ignored, false))
                .Returns(A.Fake<ISchemaEntity>());

            var trigger = new ContentChangedTrigger
            {
                Schemas = ImmutableList.Create(
                    new ContentChangedTriggerSchema()
                )
            };

            var errors = await RuleTriggerValidator.ValidateAsync(appName, trigger, appProvider);

            Assert.Empty(errors);
        }
    }
}
