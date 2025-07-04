import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Pages/login/login.component';
import { InputFieldComponent } from './CommonComponents/input-field/input-field.component';
import { DropdownFieldComponent } from './CommonComponents/dropdown-field/dropdown-field.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslatePipe } from './translate.pipe';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ContactsComponent } from './Pages/contacts/contacts.component';
import { AddEditContactComponent } from './Pages/add-edit-contact/add-edit-contact.component';
import { SpinnerComponent } from './CommonComponents/spinner/spinner.component';
import { ConfirmDialogComponent } from './CommonComponents/confirm-dialog/confirm-dialog.component';
import { ToastrModule } from 'ngx-toastr';
import { HttpRequestInterceptor } from './Services/http-request.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    InputFieldComponent,
    DropdownFieldComponent,
    TranslatePipe,
    ContactsComponent,
    AddEditContactComponent,
    SpinnerComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpRequestInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
