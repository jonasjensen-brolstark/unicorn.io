import { Component, ChangeDetectionStrategy, Output, EventEmitter, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-bid',
  templateUrl: './bid.component.html',
  styleUrls: ['./bid.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BidComponent {

  @Output() bid = new EventEmitter<number>();

  @ViewChild('bidInput')
  private input!: ElementRef<HTMLInputElement>;

  public onBid(amount: number) {
    this.bid.emit(amount);
    this.input.nativeElement.value = '';
  }

}
