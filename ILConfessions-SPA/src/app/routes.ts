import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { MessagesComponent } from './messages/messages.component';
import { ConfessionComponent } from './confession/confession.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MembersComponent },
      { path: 'messages', component: MessagesComponent },
    ]
  },
  { path: 'register', component: RegisterComponent },
  { path: 'confessions', component: ConfessionComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
