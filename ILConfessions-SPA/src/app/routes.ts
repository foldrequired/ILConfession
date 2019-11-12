import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { ConfessionComponent } from './confession/confession.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MemberProfileResolver } from './_resolvers/member-profile.resolver';
import { UnsavedChanges } from './_guards/unsaved-changes.guard';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MembersListComponent, resolve: {users: MemberListResolver}},
      { path: 'member/profile', component: MemberProfileComponent,
        resolve: {user: MemberProfileResolver}, canDeactivate: [UnsavedChanges]},
      { path: 'messages', component: MessagesComponent },
    ]
  },
  { path: 'register', component: RegisterComponent },
  { path: 'confessions', component: ConfessionComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
