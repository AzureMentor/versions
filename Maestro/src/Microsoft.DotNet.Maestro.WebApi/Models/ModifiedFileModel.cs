﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Microsoft.DotNet.Maestro.WebApi.Models
{
    public class ModifiedFileModel
    {
        public string FullPath { get; set; }

        public static bool TryParse(string repoFullName, string refSpec, string fullPath, out ModifiedFileModel model)
        {
            // the file path goes like the following:
            // <owner>/<repo>/<branch>/<fullPath>

            model = null;

            if (!refSpec.StartsWith("refs/heads/"))
            {
                Trace.TraceError($"Invalid RefSpec - expecting to start with 'refs/heads/': '{refSpec}'");
                return false;
            }

            string branchName = refSpec.Substring("refs/heads/".Length);

            model = new ModifiedFileModel()
            {
                FullPath = string.Join("/", repoFullName, branchName, fullPath)
            };

            return true;
        }
    }
}