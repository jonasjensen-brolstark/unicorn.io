import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Unicorn } from '../models/unicorn';

@Injectable({ providedIn: 'root' })
export class UnicornService {
  constructor(private httpClient: HttpClient) { }

  getUnicorns(): Observable<Unicorn[]> {
    return this.httpClient.get<Unicorn[]>('http://localhost:5000/unicorn');
  }
}
