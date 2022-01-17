import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bid } from '../models/bid';

@Injectable({
  providedIn: 'root'
})
export class BidService {

  constructor(private httpClient: HttpClient) { }

  getBids(): Observable<Bid[]> {
    return this.httpClient.get<Bid[]>(`http://localhost:5000/bids`);
  }

  createBid(amount: number): Observable<Bid> {
    let params = new HttpParams()
      .append("amount", amount);

    return this.httpClient.post<Bid>(`http://localhost:5000/bids`, undefined, { params });
  }
}
