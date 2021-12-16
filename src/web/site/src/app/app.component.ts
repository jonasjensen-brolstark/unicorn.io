import { Component } from '@angular/core';
import { Subscription, timer, switchMap } from 'rxjs';
import { Unicorn } from './models/unicorn';
import { UnicornService } from './services/unicorn.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  unicorns$ = timer(0, 2000).pipe(switchMap(() => this.unicornService.getUnicorns()));
  trackById = (_: number, unicorn: Unicorn) => unicorn.id;

  constructor(private unicornService: UnicornService) { }
}
