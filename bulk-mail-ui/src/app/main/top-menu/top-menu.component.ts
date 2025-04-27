import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Router, RouterLink, RouterLinkActive, RouterModule, RouterOutlet } from '@angular/router';
import { MainRoutingModule } from '../main-routing.module';
import { AuthService } from '../../auth/services/auth.service';

@Component({
    selector: 'app-top-menu',
    imports: [CommonModule, SharedModule, RouterLink, RouterLinkActive],
    templateUrl: './top-menu.component.html',
    styleUrl: './top-menu.component.scss'
})
export class TopMenuComponent {

  authService = inject(AuthService)
  router = inject(Router);

  isLoggedIn(){
    return this.authService.isLoggedIn()
  }

  logOut(){
    localStorage.removeItem('bulkmailtoken')
    this.router.navigate(['/'])
  }

  goToProfile(){
    this.router.navigate(['/','user-profile'])
  }
}
