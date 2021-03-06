// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Shoption.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
            new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
            new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
            new ApiResource("resource_payment"){Scopes={"payment_fullpermission"}},
            new ApiResource("resource_gateway"){Scopes={"gateway_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource()
                       {
                           Name="roles",
                           DisplayName ="Roles",
                           Description = "User Roles",
                           UserClaims = new[]{"role"}
                       }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Full access for Catalog API"),
                new ApiScope("basket_fullpermission", "Full access for Basket API"),
                new ApiScope("discount_fullpermission", "Full access for Discount API"),
                new ApiScope("order_fullpermission", "Full access for Order API"),
                new ApiScope("gateway_fullpermission", "Full access for Gateway API"),
                new ApiScope("payment_fullpermission", "Full access for Payment API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName="Mobile",
                    ClientId="MobileAppClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={
                        "catalog_fullpermission",
                        "gateway_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },
                new Client
                {
                    ClientName="Mobile",
                    ClientId="MobileAppClientForUser",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // this way we can get refresh token
                    AllowOfflineAccess = true,
                    AllowedScopes={
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId, // userId
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess, // even user is offline we can get refresh token
                        "basket_fullpermission",
                        "discount_fullpermission",
                        "order_fullpermission",
                        "gateway_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName,
                        "roles"
                    },
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute, // refresh token gonna have a specific lifetime,
                    AbsoluteRefreshTokenLifetime= (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse,
                },
                new Client
                {
                    ClientName="Token Exchange Client",
                    ClientId="TokenExchangeClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = new [] {"urn:ieft:params:oauth:grant-type:token-exchange"},
                    AllowedScopes={
                        "payment_fullpermission",
                        IdentityServerConstants.StandardScopes.OpenId,
                    }
                },
            };
    }
}