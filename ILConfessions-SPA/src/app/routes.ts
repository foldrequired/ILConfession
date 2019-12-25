import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MemberProfileResolver } from './_resolvers/member-profile.resolver';
import { UnsavedChanges } from './_guards/unsaved-changes.guard';
import { ConfessionsListComponent } from './confessions/confessions-list/confessions-list.component';
import { ConfessionListResolver } from './_resolvers/confession-list.resolver';
import { ConfessionsCreateComponent } from './confessions/confessions-create/confessions-create.component';
import { ConfessionsDetailsComponent } from './confessions/confessions-details/confessions-details.component';
import { InfoComponent } from './info/info.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'profile', component: MemberProfileComponent,
        resolve: {user: MemberProfileResolver}, canDeactivate: [UnsavedChanges]},
      // { path: 'messages', component: MessagesComponent },
      { path: 'confessions/create', component: ConfessionsCreateComponent }
    ]
  },
  { path: 'register', component: RegisterComponent },
  { path: 'info', component: InfoComponent },
  { path: 'confessions', component: ConfessionsListComponent, resolve: {confessions: ConfessionListResolver} },
  { path: 'confessions/:id', component: ConfessionsDetailsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
