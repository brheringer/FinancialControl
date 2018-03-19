import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../core/local-services/session.service';
import { LoginService } from '../../core/services/login.service';
import { UserSession } from '../../core/model/user-session';

@Component({
  selector: 'user-corner',
  templateUrl: './user-corner.component.html',
  styleUrls: ['./user-corner.component.css'],
  providers: []
})
export class UserCornerComponent implements OnInit {

  userName = '';
  isLoggedIn = false;

  constructor(private sessionService: SessionService, private loginService: LoginService) { }

  ngOnInit() {
    this.refresh();

    this.sessionService.getSessionSubject().subscribe(userSession => {
      this.refresh();
    });
  }

  doLogout() {
    this.loginService.logout();
    this.refresh();
  }

  refresh(): void {
    if (this.sessionService.hasCurrentSession()) {
      this.userName = this.sessionService.getCurrentSession().userName;
      this.isLoggedIn = true;
    }
    else {
      this.userName = '';
      this.isLoggedIn = false;
    }
  }

  //TEMP uso para olhar na requisicao se deu certo ou errado a chamada de um metodo com [Authorize]
  test() {
    this.loginService.getUser(1).subscribe(dto => {
      console.log(dto);
    });
  }
}
