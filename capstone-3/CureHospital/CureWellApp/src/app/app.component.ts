import { Component } from '@angular/core';

/*
  Root component. main.ts bootstraps AppModule, and AppModule
  bootstraps this class. It has no fields of its own.
  The navbar and <router-outlet> live in app.component.html.
  routerLink and router-outlet work because app.module.ts imports
  the routing table from app.routing.ts.
*/
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent { }
