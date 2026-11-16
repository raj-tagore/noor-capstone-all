import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ViewSurgeryComponent } from './curewell-components/view-surgery/view-surgery.component';
import { UpdateSurgeryComponent } from './curewell-components/update-surgery/update-surgery.component';
import { routing } from './app.routing';

/*
  Registers the pieces the templates need.

  declarations
    The three components. A template can only use a component that
    is declared here.

  imports
    BrowserModule     date pipe in view-surgery.component.html, and the browser renderer
    FormsModule       [(ngModel)], #field="ngModel", and (ngSubmit) on the update form
    HttpClientModule  HttpClient used inside curewell.service.ts
    routing           the URL map from app.routing.ts, including routerLink and router-outlet

  bootstrap
    AppComponent is the first component. Its selector app-root is the
    tag in src/index.html.
*/
@NgModule({
  declarations: [AppComponent, ViewSurgeryComponent, UpdateSurgeryComponent],
  imports: [BrowserModule, FormsModule, HttpClientModule, routing],
  bootstrap: [AppComponent]
})
export class AppModule { }
