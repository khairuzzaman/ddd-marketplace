using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Marketplace.Tests
{
    public class ClassifiedAd_Publish_Spec
    {
        private readonly ClassifiedAd _classifiedAd;

        public ClassifiedAd_Publish_Spec()
        {
            _classifiedAd = new ClassifiedAd(new ClassifiedAdId(Guid.NewGuid()), new UserId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_publish_a_valid_ad()
        {
            _classifiedAd.SetTitle(ClassifiedAddTitle.FromString("Test Ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100.10M, "EUR", new FakeCurrencyLookup()));
            _classifiedAd.RequestToPublish();

            Assert.Equal(ClassifiedAd.ClassifiedAdState.PendingReview, _classifiedAd.State);
        }

        [Fact]
        public void Cannot_publish_without_title()
        {
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100.10M, "EUR", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_text()
        {
            _classifiedAd.SetTitle(ClassifiedAddTitle.FromString("Test Ad"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100.10M, "EUR", new FakeCurrencyLookup()));

            Assert.Equal(ClassifiedAd.ClassifiedAdState.PendingReview, _classifiedAd.State);
        }

        [Fact]
        public void Cannot_publish_without_price()
        {
            _classifiedAd.SetTitle(ClassifiedAddTitle.FromString("Test Ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));

            Assert.Equal(ClassifiedAd.ClassifiedAdState.PendingReview, _classifiedAd.State);
        }

        [Fact]
        public void Cannot_publish_with_zero_price()
        {
            _classifiedAd.SetTitle(ClassifiedAddTitle.FromString("Test Ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(0.0M, "EUR", new FakeCurrencyLookup()));

            Assert.Equal(ClassifiedAd.ClassifiedAdState.PendingReview, _classifiedAd.State);
        }
    }
}
