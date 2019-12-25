import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ModalModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { TimeAgoPipe } from 'time-ago-pipe';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MemberProfileResolver } from './_resolvers/member-profile.resolver';
import { ConfessionsListComponent } from './confessions/confessions-list/confessions-list.component';
import { ConfessionListResolver } from './_resolvers/confession-list.resolver';
import { UnsavedChanges } from './_guards/unsaved-changes.guard';
import { AuthGuard } from './_guards/auth.guard';
import { ConfessionsCreateComponent } from './confessions/confessions-create/confessions-create.component';
import { ConfessionsDetailsComponent } from './confessions/confessions-details/confessions-details.component';
import { InfoComponent } from './info/info.component';
import { FooterComponent } from './footer/footer.component';
import { LoginModalComponent } from './nav/login-modal/login-modal.component';

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
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MessagesComponent,
      MemberProfileComponent,
      ConfessionsListComponent,
      ConfessionsCreateComponent,
      ConfessionsDetailsComponent,
      InfoComponent,
      FooterComponent,
      LoginModalComponent,
      TimeAgoPipe
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      PaginationModule.forRoot(),
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TabsModule.forRoot(),
      ModalModule.forRoot(),
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
      MemberProfileResolver,
      ConfessionListResolver,
      AuthGuard,
      UnsavedChanges,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
   ],
   entryComponents: [
    LoginModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
