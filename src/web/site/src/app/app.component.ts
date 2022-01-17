import { Component, ElementRef, ViewChild } from '@angular/core';
import { timer, switchMap } from 'rxjs';
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

  @ViewChild('bidInput')
  private input!: ElementRef<HTMLInputElement>;

  constructor(private unicornService: UnicornService, private bidService: BidService) { }

  public bid(amount: string) {
    this.bidService.createBid(parseFloat(amount)).subscribe();
    this.input.nativeElement.value = '';
  }
}
