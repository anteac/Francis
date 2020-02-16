import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ShowLogMessageDialog } from '../../dialogs/show-log-message/show-log-message.dialog';
import { LogEventLevel } from '../../models/generated/LogEventLevel';
import { LogMessage } from '../../models/generated/LogMessage';
import { MessengerService } from '../../services/messenger.service';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent {

  LogEventLevel = LogEventLevel;

  error: string;
  logFiles: string[] = [];
  logs: LogMessage[];
  currentLogFile: string;

  constructor(private http: HttpClient, private messenger: MessengerService, public dialog: MatDialog) {
    this.getLogFiles();
  }

  getLogFiles(): void {
    this.messenger.loading.next(true);
    this.http.get<string[]>('monitoring/logs').subscribe(logFiles => {
      this.logFiles = logFiles;
      this.currentLogFile = logFiles[0];
      this.loadLogFile();
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured while retrieving list of log files.'
      this.messenger.loading.next(false);
    });
  }

  loadLogFile(): void {
    this.messenger.loading.next(true);
    this.http.get<LogMessage[]>(`monitoring/logs/${this.currentLogFile}`).subscribe(logs => {
      this.logs = logs;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured while retrieving list of logs.'
      this.messenger.loading.next(false);
    });
  }

  showLog(log: LogMessage) {
    this.dialog.open(ShowLogMessageDialog, { data: log });
  }
}
