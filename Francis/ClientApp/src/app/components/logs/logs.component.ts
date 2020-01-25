import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MessengerService } from '../../services/messenger/messenger.service';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent {

  error: string;
  logFiles: string[] = [];

  constructor(private http: HttpClient, private messenger: MessengerService) {
    this.getLogFiles();
  }

  getLogFiles(): void {
    this.messenger.loading.next(true);
    this.http.get<string[]>('monitoring/logs').subscribe(logFiles => {
      this.logFiles = logFiles;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured while retrieving list of log files.'
      this.messenger.loading.next(false);
    });
  }

  openLogFile(logFile: string): void {
    window.open(`monitoring/logs/${logFile}`, '_blank');
  }

}
