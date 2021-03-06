﻿// ==========================================================================
//  IApps.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squidex.Domain.Apps.Read.Apps;
using Squidex.Domain.Apps.Read.Rules;
using Squidex.Domain.Apps.Read.Schemas;

namespace Squidex.Domain.Apps.Read
{
    public interface IAppProvider
    {
        Task<(IAppEntity, ISchemaEntity)> GetAppWithSchemaAsync(string appName, Guid id);

        Task<IAppEntity> GetAppAsync(string appName);

        Task<ISchemaEntity> GetSchemaAsync(string appName, Guid id, bool provideDeleted = false);

        Task<ISchemaEntity> GetSchemaAsync(string appName, string name, bool provideDeleted = false);

        Task<List<ISchemaEntity>> GetSchemasAsync(string appName);

        Task<List<IRuleEntity>> GetRulesAsync(string appName);

        Task<List<IAppEntity>> GetUserApps(string userId);
    }
}
