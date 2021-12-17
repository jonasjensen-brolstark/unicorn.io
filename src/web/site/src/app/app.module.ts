import { ErrorHandler, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router'
import { BrowserModule } from '@angular/platform-browser';
import { ApmErrorHandler, ApmModule, ApmService } from '@elastic/apm-rum-angular';

import { AppComponent } from './app.component';

const routes: Routes = []

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    ApmModule,
    BrowserModule,
    RouterModule.forRoot(routes),
    HttpClientModule
  ],
  providers: [
    ApmService,
    {
      provide: ErrorHandler,
      useClass: ApmErrorHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(service: ApmService) {
    service.init({
      serviceName: 'site',
      serverUrl: 'http://localhost:8200',
      environment: 'production',
    });
  }
}
