import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { SharedModule } from './shared';
import { CoreModule } from './core';

import { HomeModule } from './home/home.module';

import { routing } from './app.routes';
import { AppService } from './app.service';
import { AppComponent } from './app.component';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        routing,
        NgbModule.forRoot(),
        CoreModule.forRoot(),
        SharedModule,
        HomeModule,
    ],
    providers: [
        AppService
    ],
    exports: [
        SharedModule,
        CoreModule,
        NgbModule
    ]
})
export class AppModule { }
