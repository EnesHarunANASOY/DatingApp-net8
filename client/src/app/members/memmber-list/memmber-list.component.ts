import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-memmber-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './memmber-list.component.html',
  styleUrl: './memmber-list.component.css'
})
export class MemmberListComponent implements OnInit {

  private memberService = inject(MembersService);
  membersOnHome : Member[] = [];

  ngOnInit(): void{

    this.loadMembers();
  }

  loadMembers(){
    this.memberService.getMembers().subscribe({
      next: membersAreNExt => this.membersOnHome = membersAreNExt
    })
  }
}
