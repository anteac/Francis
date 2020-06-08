import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { BotUser } from '../../models/generated/BotUser';
import { MessengerService } from '../../services/messenger.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {

  error: string;
  users: BotUser[];

  constructor(private http: HttpClient, private messenger: MessengerService, public dialog: MatDialog) {
    this.getUsers();
  }

  getUsers(): void {
    this.messenger.loading.next(true);
    this.http.get<BotUser[]>('tracking/users').subscribe(users => {
      this.users = users;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured while retrieving list of users.'
      this.messenger.loading.next(false);
    });
  }
}
