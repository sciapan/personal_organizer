import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Birthday } from '../Birthday';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class BirthdaysService {
  private apiUrl = 'https://localhost:7212'

  constructor(private http: HttpClient) { }

  getBirthdays(): Observable<Birthday[]> {
    return this.http.get<Birthday[]>(`${this.apiUrl}/birthday`);
  }

  deleteBirthday(birthday: Birthday): Observable<Birthday> {
    const url = `${this.apiUrl}/birthday/${birthday.id}`;
    return this.http.delete<Birthday>(url);
  }

  updateBirthday(birthday: Birthday): Observable<Object> {
    const url = `${this.apiUrl}/birthday/${birthday.id}`;
    return this.http.put(url, birthday, httpOptions);
  }

  addBirthday(birthday: Birthday): Observable<Birthday> {
    const url = `${this.apiUrl}/birthday`;
    return this.http.post<Birthday>(url, birthday, httpOptions);
  }
}