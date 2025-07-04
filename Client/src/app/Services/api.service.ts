import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../Interface/CommonInterface';
import { Token } from '@angular/compiler';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/Authentication/login?user=${username}&password=${password}`,
      null,
      { responseType: 'text' as 'text' }
    );
  }

  getContacts(): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http.get(`${this.baseUrl}/api/Contact/GetAllContacts`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  addOrUpdateContact(contact: Contact): Observable<any> {
    const token = localStorage.getItem('token');

    return this.http.post(
      `${this.baseUrl}/api/Contact/AddOrUpdateContact`,
      contact,
      {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }
    );
  }

  removeContact(contact: Contact[]): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http.post(`${this.baseUrl}/api/Contact/RemoveContacts`, contact, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
  }

}
