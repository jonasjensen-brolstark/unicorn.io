import { Component, ElementRef, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { timer, switchMap, Observable, map } from 'rxjs';
import { Unicorn } from './models/unicorn';
import { BidService } from './services/bid.service';
import { UnicornService } from './services/unicorn.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  unicorns$ = timer(0, 2000).pipe(switchMap(() => this.unicornService.getUnicorns()));
  trackById = (_: number, unicorn: Unicorn) => unicorn.id;

  bids$ = timer(0, 2000).pipe(switchMap(() => this.bidService.getBids()));

  constructor(private unicornService: UnicornService, private bidService: BidService) { }

  public onBid(amount: number, unicornId: string) {
    this.bidService.createBid(amount, unicornId).subscribe();
  }
}
