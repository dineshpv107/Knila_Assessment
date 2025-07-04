import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Router } from '@angular/router'; // Import Router to navigate to login
import { SpinnerService } from './spinner.service'; // Assuming your SpinnerService is imported

@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {

    constructor(private spinnerService: SpinnerService, private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.spinnerService.show();

        const token = localStorage.getItem('token');
        const clonedRequest = token ? req.clone({
            headers: req.headers.set('Authorization', `Bearer ${token}`)
        }) : req;

        return next.handle(clonedRequest).pipe(
            catchError((error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.router.navigate(['/login']);
                }
                return throwError(error);
            }),
            finalize(() => {
                this.spinnerService.hide();
            })
        );
    }
}
