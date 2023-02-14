import { Component, OnInit } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser';

import { Params, ActivatedRoute, Router } from '@angular/router';

import { routerTransition } from './router.animations';
import { ExternalLoginStatus } from './app.models';
import { AppService } from './app.service';
import { AuthService } from './core';

@Component({
  selector: 'appc-root',
  animations: [routerTransition],
  styleUrls: ['./app.component.scss'],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  public notificationOptions = {
    position: ['top', 'right'],
    timeOut: 5000,
    lastOnBottom: true
  };
  constructor(
    private accountService: AuthService,
    private router: Router,
    private title: Title,
    private meta: Meta,
    private appService: AppService,
    private activatedRoute: ActivatedRoute
  ) { }

  public ngOnInit() {
    this.updateTitleAndMeta();
    if (window.location.href.indexOf('?postLogout=true') > 0) {
      this.accountService.signoutRedirectCallback().then(() => {
        const url: string = this.router.url.substring(
          0,
          this.router.url.indexOf('?')
        );
        this.router.navigateByUrl(url);
      });
    }

    this.activatedRoute.queryParams.subscribe((params: Params) => {
      const param = params['externalLoginStatus'];
      if (param) {
        const status = <ExternalLoginStatus>+param;
        switch (status) {
          case ExternalLoginStatus.CreateAccount:
            this.router.navigate(['createaccount']);
            break;
          default:
            break;
        }
      }
    });
  }

  public getState(outlet: any) {
    return outlet.activatedRouteData.state;
  }

  private updateTitleAndMeta() {
    this.title.setTitle(this.appService.appData.content['app_title']);
    this.meta.addTags([
      { name: 'description', content: this.appService.appData.content['app_description'] },
      { property: 'og:title', content: this.appService.appData.content['app_title'] },
      { property: 'og:description', content: this.appService.appData.content['app_description'] }
    ]);
  }
}
