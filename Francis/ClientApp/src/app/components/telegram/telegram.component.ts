import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AboutTelegramBot } from '../../models/generated/AboutTelegramBot';
import { TelegramOptions } from '../../models/generated/TelegramOptions';
import { MessengerService } from '../../services/messenger/messenger.service';

@Component({
  selector: 'app-telegram',
  templateUrl: './telegram.component.html',
  styleUrls: ['./telegram.component.scss']
})
export class TelegramComponent {

  options: TelegramOptions = {};
  about: AboutTelegramBot = null;
  error: string = null;

  constructor(private http: HttpClient, private messenger: MessengerService) {
    this.getOptions();
  }

  getOptions(): void {
    this.messenger.loading.next(true);
    this.http.get('options/telegram').subscribe(options => {
      this.options = options;
      this.messenger.loading.next(false);
    });
  }

  pushOptions(): void {
    this.about = null;
    this.error = null;

    this.messenger.loading.next(true);
    this.http.post('options/telegram', this.options).subscribe(about => {
      this.about = about;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured. Please check your settings.'
      this.messenger.loading.next(false);
    });
  }

}
