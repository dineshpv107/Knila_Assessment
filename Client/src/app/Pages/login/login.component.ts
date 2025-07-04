import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/Services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginForm: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder, private apiService: ApiService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.loginForm.invalid) return;

    console.log('Form Submitted:', this.loginForm.value);
    this.apiService.login(this.f['username'].value, this.f['password'].value).subscribe({
      next: (response) => {
        console.log('Login successful:', response);
        localStorage.setItem('token', response);
        this.router.navigate(['/contact']);
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }
}
