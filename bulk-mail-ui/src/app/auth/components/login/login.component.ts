import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Subscription } from 'rxjs';
import { SharedModule } from '../../../shared/shared.module';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit, OnDestroy {
  subscriptions: Subscription = new Subscription();
  toggleText = signal('show')
  displayLoading: boolean = false
  authService = inject(AuthService)
  router = inject(Router)
  
  loginForm: FormGroup = this.fb.group({
    userName: ['',[Validators.required]],
    password: ['', [Validators.required]]
  })
  
  constructor(private fb: FormBuilder){}
  // constructor(private fb: FormBuilder, private authService: AuthService, private spinner: NgxSpinnerService, private router: Router, private cookieService: CookieService){}

  ngOnInit(): void {
    // throw new Error('Method not implemented.');
  }
  ngOnDestroy(): void {
    // throw new Error('Method not implemented.');
  }

  login(){
    let formVal = this.loginForm.getRawValue()
    let body: any = {
      Email: formVal.userName,
      Password: formVal.password
    }

    // this.spinner.show();
    
    this.displayLoading = true;

    this.authService.login(body).subscribe({
      next: (res: any) => {
        localStorage.setItem('bulkmailtoken',res.token)
        this.displayLoading = false
        // Swal.fire(AppConfig.AppName,res.message,'success')
        // this.authService.getUserInfo()
        this.router.navigate([''])
      },
      error: (error: any) => {
        this.displayLoading = false
        if (error.message) {
          // Swal.fire(AppConfig.AppName,error.message,'error')
        }
        console.error('login error',error)
      }
    })
  }

  togglePasswordDisplay(){
    const passwordInput = document.getElementById("password")!
    const passwordToggler = document.getElementById("eye-span")!

    const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordInput.setAttribute('type', type);

    passwordInput.getAttribute('type') === 'password' ? this.toggleText.set('show') : this.toggleText.set('hide');
    // passwordToggler.className = ''
    // passwordToggler.className = passwordInput.getAttribute('type') === 'password' ? 'fa-solid fa-eye' : 'fa-regular fa-eye-slash';
  }
}
