import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { EnhancedBotUser } from '../../models/generated/EnhancedBotUser';
import { MessengerService } from '../../services/messenger.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {

  error: string;
  users: EnhancedBotUser[];

  constructor(private http: HttpClient, private messenger: MessengerService, public dialog: MatDialog) {
    this.getUsers();
  }

  getUsers(): void {
    this.messenger.loading.next(true);
    this.http.get<EnhancedBotUser[]>('tracking/users').subscribe(users => {
      this.users = users;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured while retrieving list of users.'
      this.messenger.loading.next(false);
    });
  }
}
