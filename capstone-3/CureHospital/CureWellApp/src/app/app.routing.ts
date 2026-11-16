import { RouterModule } from '@angular/router';
import { ViewSurgeryComponent } from './curewell-components/view-surgery/view-surgery.component';
import { UpdateSurgeryComponent } from './curewell-components/update-surgery/update-surgery.component';

/*
  URL map for the app. Imported by app.module.ts and attached to
  the router. <router-outlet> in app.component.html is where the
  matching component's template is inserted.

  viewSurgery
    ViewSurgeryComponent and view-surgery.component.html.
    Also the target of the navbar links and of Cancel / successful update.

  editSurgery/:surgeryId/:specializationName/:surgeryDate/:startTime/:endTime/:doctorName/:doctorId/:surgeryCategory
    Each :name is one segment of the URL. ViewSurgeryComponent.editSurgery()
    navigates with the values in this same order. UpdateSurgeryComponent.ngOnInit()
    reads them back with route.snapshot.params['surgeryId'] and the other names.

  '' and '**'
    Opening localhost:4200, or any unknown path, lands on the list page.
*/
export const routing = RouterModule.forRoot([
  { path: 'viewSurgery', component: ViewSurgeryComponent },
  { path: 'editSurgery/:surgeryId/:specializationName/:surgeryDate/:startTime/:endTime/:doctorName/:doctorId/:surgeryCategory', component: UpdateSurgeryComponent },
  { path: '', redirectTo: 'viewSurgery', pathMatch: 'full' },
  { path: '**', redirectTo: 'viewSurgery' }
]);
