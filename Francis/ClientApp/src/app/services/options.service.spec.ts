import { TestBed } from '@angular/core/testing';
import { OmbiService, TelegramService } from './options.service';

describe('OptionsContext', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const telegram: TelegramService = TestBed.get(TelegramService);
    expect(telegram).toBeTruthy();

    const ombi: OmbiService = TestBed.get(OmbiService);
    expect(ombi).toBeTruthy();

  });
});
