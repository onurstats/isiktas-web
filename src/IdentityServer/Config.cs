// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("isiktas-api", "Isiktas Web Api")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                
                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "isiktas-vue-spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://localhost:3000",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,


                    RedirectUris =
                    {
                        "http://localhost:3000/oidc-callback",
                        "http://localhost:3000/silent-renew-oidc.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:3000" },
                    AllowedCorsOrigins = { "http://localhost:3000" },

                    AllowedScopes = { "openid", "profile", "isiktas-api","email" }
                }
            };
    }
}