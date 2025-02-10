import { Component, computed, inject, input } from '@angular/core';
import { Member } from '../../_models/member';
import { RouterLink } from '@angular/router';
import { LikesService } from '../../_services/likes.service';
import { PresenceService } from '../../_services/presence.service';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
  private likeService =inject(LikesService);
  private presenceService = inject(PresenceService);
  memberOnCard = input.required<Member>();
  hasLiked = computed(()=> this.likeService.likeIds().includes(this.memberOnCard().id));
  isOnline = computed(()=> this.presenceService.onlineUsers().includes(this.memberOnCard().username));  

  toggleLike(){
    this.likeService.toggleLike(this.memberOnCard().id).subscribe({
      next: () => {
        if(this.hasLiked()){
          this.likeService.likeIds.update(ids => ids.filter(x=> x !== this.memberOnCard().id))
        }
        else{
          this.likeService.likeIds.update(ids=> [...ids, this.memberOnCard().id])
        }
      }
    })
  }

}
