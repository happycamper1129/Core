import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AppService {
    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
    getData(): Promise<any> {
        return new Promise((resolve) => {
            this.httpClient.get<any>(`${this.baseUrl}api/applicationdata`)
                .subscribe(res => {
                    // console.log(res);
                    (<any>window).appData = {
                        content: JSON.parse(res.content),
                        cultures: res.cultureItems,
                        loginProviders: res.loginProviders
                    };
                    resolve(res);

                });
        });
    }
}
