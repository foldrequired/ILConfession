import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';

import { AppComponent } from './app.component';
import { ConfessionComponent } from './confession/confession.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberProfileResolver } from './_resolvers/member-profile.resolver';
import { UnsavedChanges } from './_guards/unsaved-changes.guard';
import { AuthGuard } from './_guards/auth.guard';
import { MemberProfilePhotoComponent } from './members/member-profile-photo/member-profile-photo.component';

export function tokenGet() {
  return localStorage.getItem('token');
}

// Fix NgxGallery hammer error
export class CustomHammerConfig extends HammerGestureConfig {
  overrides = {
    pinch: { enable: false },
    rotate: { enable: false }
  };
}

@NgModule({
   declarations: [
      AppComponent,
      ConfessionComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MessagesComponent,
      MembersListComponent,
      MemberProfileComponent,
      MemberProfilePhotoComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TabsModule.forRoot(),
      NgxGalleryModule,
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGet,
          whitelistedDomains: ['localhost:5000', 'localhost:5001']  ,
          blacklistedRoutes: ['localhost:5000/api/v1/auth']
        }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      MemberListResolver,
      MemberProfileResolver,
      AuthGuard,
      UnsavedChanges,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
