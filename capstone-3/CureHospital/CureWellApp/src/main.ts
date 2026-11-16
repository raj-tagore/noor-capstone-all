import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';

/*
  Application entry. Angular compiles this first.
  bootstrapModule(AppModule) creates the module in app.module.ts,
  which then creates AppComponent and attaches it to <app-root>
  in index.html. Routing, forms, and HTTP are switched on there,
  and the first URL redirect in app.routing.ts shows View Surgery.
*/
platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
