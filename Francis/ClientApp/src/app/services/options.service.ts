import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AboutOmbi } from '../models/generated/AboutOmbi';
import { AboutTelegramBot } from '../models/generated/AboutTelegramBot';
import { OmbiOptions } from '../models/generated/OmbiOptions';
import { TelegramOptions } from '../models/generated/TelegramOptions';
import { MessengerService } from '../services/messenger.service';

@Injectable({
  providedIn: 'root'
})
abstract class OptionsService<TOptions, TAbout> {
  options: TOptions = <TOptions>{};
  about: TAbout = null;
  error: string = null;

  protected abstract get endpoint(): string;

  constructor(
    protected http: HttpClient,
    protected messenger: MessengerService,
  ) { }

  getOptions(): void {
    this.messenger.loading.next(true);
    this.http.get<TOptions>(`options/${this.endpoint}`).subscribe(options => {
      this.options = options;
      this.messenger.loading.next(false);
    }, () => {
      this.messenger.loading.next(false);
    });
  }

  pushOptions(): void {
    this.about = null;
    this.error = null;

    this.messenger.loading.next(true);
    this.http.post<TAbout>(`options/${this.endpoint}`, this.options).subscribe(about => {
      this.about = about;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured. Please check your settings.'
      this.messenger.loading.next(false);
    });
  }
}

@Injectable({
  providedIn: 'root'
})
export class TelegramService extends OptionsService<TelegramOptions, AboutTelegramBot> {

  protected get endpoint(): string {
    return "telegram";
  }

}

@Injectable({
  providedIn: 'root'
})
export class OmbiService extends OptionsService<OmbiOptions, AboutOmbi> {

  protected get endpoint(): string {
    return "ombi";
  }

}
