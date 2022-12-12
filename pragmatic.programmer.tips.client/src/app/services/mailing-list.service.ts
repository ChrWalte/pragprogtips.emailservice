import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MailingListService {
  private baseApi = '/api/mailing-list';

  constructor(private http: HttpClient) {}

  subscribe(email: string, name: string | undefined): Observable<string> {
    let api = `${this.baseApi}/subscribe?email=${email}`;
    if (name) {
      api += `&name=${name}`;
    }

    return this.http.get(api).pipe(map((response: any) => response as string));
  }

  unsubscribe(email: string): Observable<string> {
    return this.http
      .get(`${this.baseApi}/unsubscribe?email=${email}`)
      .pipe(map((response: any) => response as string));
  }
}
