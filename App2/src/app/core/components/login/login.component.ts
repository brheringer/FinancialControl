import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AlertService } from '../../local-services/alert.service';
import { LoginService } from '../../services/login.service'
import { SessionService } from '../../local-services/session.service'

import { User } from '../../model/user';
import { UserSession } from '../../model/user-session';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model = new User();
  returnUrl = '/';
  isLoggedIn = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private loginService: LoginService,
    private sessionService: SessionService) { }

  ngOnInit() {
    //this.authenticationService.logout(); // reset login status

    //TODO rever:
    //this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/'; // get return url from route parameters or default to '/'
    this.isLoggedIn = this.sessionService.hasCurrentSession();
    if (this.isLoggedIn)
      this.model.userName = this.sessionService.getCurrentSession().userName;
  }

  doLogin() {
    this.loginService.login(this.model).subscribe(
      dto => {
        if (dto.response.hasException)
        {
          //console.log(dto.response.exception); //TODO emporário
          //alert(dto.response.exception);
          this.alertService.error(dto.response.exception);
        }
        else {
          this.isLoggedIn = true;
          //console.log("login ok"); //TODO temporário
          //alert("ok");
          this.alertService.success("ok!"); //TODO temporário
          this.router.navigate([this.returnUrl]);
        }
      },
      err => {
        console.log("O interceptor não deveria deixar esse caso acontecer...");
      });
  }

  doLogout() {
    this.loginService.logout();
    this.isLoggedIn = false;
    //TODO
  }
}
