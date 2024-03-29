// 
// Copyright (c) 2004-2010 Jaroslaw Kowalski <jaak@jkowalski.net>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

namespace NLog.Layouts
{
    using NLog.Config;
    using NLog.Internal;

    /// <summary>
    /// A specialized layout that supports header and footer.
    /// </summary>
    [Layout("LayoutWithHeaderAndFooter")]
    [ThreadAgnostic]
    public class LayoutWithHeaderAndFooter : Layout
    {
        /// <summary>
        /// Gets or sets the body layout (can be repeated multiple times).
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public Layout Layout { get; set; }

        /// <summary>
        /// Gets or sets the header layout.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public Layout Header { get; set; }

        /// <summary>
        /// Gets or sets the footer layout.
        /// </summary>
        /// <docgen category='Layout Options' order='10' />
        public Layout Footer { get; set; }

        /// <summary>
        /// Precalculates the layout for the specified log event and stores the result
        /// in per-log event cache.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        /// <remarks>
        /// Calling this method enables you to store the log event in a buffer
        /// and/or potentially evaluate it in another thread even though the
        /// layout may contain thread-dependent renderer.
        /// </remarks>
        public override void Precalculate(LogEventInfo logEvent)
        {
            Layout.Precalculate(logEvent);
            if (this.Header != null)
            {
                this.Header.Precalculate(logEvent);
            }

            if (this.Footer != null)
            {
                this.Footer.Precalculate(logEvent);
            }
        }

        /// <summary>
        /// Renders the layout for the specified logging event by invoking layout renderers.
        /// </summary>
        /// <param name="logEvent">The logging event.</param>
        /// <returns>The rendered layout.</returns>
        protected override string GetFormattedMessage(LogEventInfo logEvent)
        {
            return this.Layout.Render(logEvent);
        }
    }
}
