﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XamarinBookStoreApi.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using XamarinBookStoreApi.Permissions;

namespace XamarinBookStoreApi.Blazor.Menus
{
  public class XamarinBookStoreApiMenuContributor : IMenuContributor
  {
    private readonly IConfiguration _configuration;

    public XamarinBookStoreApiMenuContributor(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
      if (context.Menu.Name == StandardMenus.Main)
      {
        await ConfigureMainMenuAsync(context);
      }
      else if (context.Menu.Name == StandardMenus.User)
      {
        await ConfigureUserMenuAsync(context);
      }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
      var l = context.GetLocalizer<XamarinBookStoreApiResource>();

      var bookStoreMenu = new ApplicationMenuItem(
          "BooksStore",
          l["Menu:BookStore"],
          icon: "fa fa-book"
      );

      context.Menu.AddItem(bookStoreMenu);

      //CHECK the PERMISSION
      if (await context.IsGrantedAsync(XamarinBookStoreApiPermissions.Books.Default))
      {
        bookStoreMenu.AddItem(new ApplicationMenuItem(
            "BooksStore.Books",
            l["Menu:Books"],
            url: "/books"
        ));
      }
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
      var accountStringLocalizer = context.GetLocalizer<AccountResource>();
      var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

      var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

      if (currentUser.IsAuthenticated)
      {
        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["ManageYourProfile"],
            $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null));
      }

      return Task.CompletedTask;
    }
  }
}
