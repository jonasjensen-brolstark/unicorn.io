import { Component, ChangeDetectionStrategy, Input, Output, EventEmitter } from '@angular/core';
import { Unicorn } from 'src/app/models/unicorn';

@Component({
  selector: 'app-unicorn',
  templateUrl: './unicorn.component.html',
  styleUrls: ['./unicorn.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UnicornComponent {

  @Input() unicorn!: Unicorn;
  @Output() bid = new EventEmitter<{ amount: number, unicornId: string }>();

  onBid(amount: number) {
    this.bid.emit({ amount, unicornId: this.unicorn.id });
  }

}
