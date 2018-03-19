import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { SessionService } from '../core/local-services/session.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private session: SessionService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.session.hasCurrentSession()) {
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    //TODO talvez ao inves de usar o caminho '/login' seja possivel so chamar uma funcao do app-routing.module
    return false;
  }
}
