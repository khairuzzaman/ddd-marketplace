﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ClassifiedAd : Entity
    {
        public ClassifiedAdId Id { get; private set; }

        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Apply(new Events.ClassifiedAdCreated
            {
                Id = id,
                OwnerId = ownerId
            });
        }

        public void SetTitle(ClassifiedAddTitle title) 
        {
            Apply(new Events.ClassifiedAdTitleChanged
            {
                Id = Id,
                Title = title
            });
        }
        public void UpdateText(ClassifiedAdText text) 
        {
            Apply(new Events.ClassifiedAdTextUpdated
            {
                Id = Id,
                AdText = text
            });
        }
        public void UpdatePrice(Price price)
        {
            Apply(new Events.ClassifiedAdPriceUpdated
            {
                Id = Id,
                Price = price.Amount,
                CurrencyCode = price.CurrencyCode
            });
        }

        public void RequestToPublish()
        {
            Apply(new Events.ClassifiedAdSentForReview
            {
                Id = Id
            });
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ClassifiedAdCreated e:
                    Id = new ClassifiedAdId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
                    break;
                case Events.ClassifiedAdTitleChanged e:
                    Title = new ClassifiedAddTitle(e.Title);
                    break;
                case Events.ClassifiedAdTextUpdated e:
                    Text = new ClassifiedAdText(e.AdText);
                    break;
                case Events.ClassifiedAdPriceUpdated e:
                    //Price = new Price(e.Price, e.CurrencyCode);
                    break;
                case Events.ClassifiedAdSentForReview e:
                    State = ClassifiedAdState.PendingReview;
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            var valid =
                Id != null &&
                OwnerId != null &&
                (State switch
                {
                    ClassifiedAdState.PendingReview =>
                        Title != null &&
                        Text != null &&
                        Price?.Amount > 0,
                    ClassifiedAdState.Active =>
                        Title != null &&
                        Text != null &&
                        Price?.Amount > 0 &&
                        ApprovedBy != null,
                    _ => true
                });
            if (!valid)
                throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");
        }

        public UserId OwnerId { get; private set; }
        public ClassifiedAddTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; set; }
        public ClassifiedAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }

        public enum ClassifiedAdState
        {
            PendingReview,
            Active,
            Inactive,
            MarkedAsSold
        }
    }
}
