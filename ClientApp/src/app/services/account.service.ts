import { Injectable } from '@angular/core';
// import { OAuthService } from 'angular-oauth2-oidc';
import { JwtHelper } from 'angular2-jwt';

import { ProfileModel } from '../models/profile-model';

@Injectable()
export class AccountService {
    public jwtHelper: JwtHelper = new JwtHelper();

    constructor(
        // private oAuthService: OAuthService
    ) { }

    public get isLoggedIn(): boolean {
        // return this.oAuthService.hasValidAccessToken();
        return false;
    }
    public get user(): ProfileModel | undefined {
        if (this.idToken) {
            return this.jwtHelper.decodeToken(this.idToken);
        }
        return undefined;
    }
    public get accessToken(): string {
        // return this.oAuthService.getAccessToken();
        return '';
    }
    // Used to access user information
    public get idToken(): string {
        // return this.oAuthService.getIdToken();
        return '';
    }
}
